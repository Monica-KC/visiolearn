from fastapi import FastAPI
from pydantic import BaseModel
from langchain.llms import HuggingFaceHub
from langchain.embeddings import HuggingFaceEmbeddings
from langchain.vectorstores import FAISS
from langchain.document_loaders import TextLoader
from langchain.chains import RetrievalQA
from langchain.prompts import PromptTemplate
import os
from pyngrok import ngrok
from dotenv import load_dotenv

# Load environment variables from .env file
load_dotenv()

# Get ngrok auth token from environment variables
ngrok_auth_token = os.getenv("NGROK_AUTH_TOKEN")
if not ngrok_auth_token:
    raise ValueError("ngrok authentication token is not set in the .env file.")

# Authenticate ngrok
ngrok.set_auth_token(ngrok_auth_token)

# Initialize FastAPI app
app = FastAPI()

# HuggingFace Hub API token
huggingfacehub_api_token = os.getenv("HUGGINGFACEHUB_API_TOKEN")
if not huggingfacehub_api_token:
    raise ValueError("Hugging Face API token is not set in the .env file.")

# Define input model for API queries
class Query(BaseModel):
    question: str

# Load RAG pipeline once when the app starts
qa_chain = None
text_folder_path = "C:/Users/singe/OneDrive/Desktop/RAG_project/data"  # Update this to your folder path

@app.get("/")
async def root():
    """
    Root endpoint to confirm the API is running.
    """
    return {"message": "Welcome to the RAG API!"}

@app.on_event("startup")
async def startup_event():
    """
    Setup RAG pipeline during application startup.
    """
    global qa_chain
    qa_chain = setup_rag_pipeline(text_folder_path)

@app.post("/query")
async def query_rag(query: Query):
    """
    Handle user queries using the RAG pipeline.
    """
    if not qa_chain:
        return {"error": "RAG pipeline is not set up yet."}
    
    # Use the RAG pipeline to generate the answer for the user's query
    try:
        answer = qa_chain.run(query.question)
        return {"answer": answer}
    except Exception as e:
        return {"error": f"Failed to process the query: {str(e)}"}

def load_texts_from_folder(folder_path):
    """
    Loads all `.txt` files from a folder and combines their contents into a single string.
    """
    documents = []
    for file_name in os.listdir(folder_path):
        if file_name.endswith(".txt"):
            file_path = os.path.join(folder_path, file_name)
            with open(file_path, "r", encoding="utf-8") as f:
                documents.append(f.read())
    return documents

def setup_rag_pipeline(folder_path):
    """
    Sets up the Retrieval-Augmented Generation pipeline:
    1. Loads text files from a folder
    2. Embeds the documents
    3. Builds a FAISS index
    4. Sets up the retriever and QA chain
    """
    # Step 1: Load text files
    document_texts = load_texts_from_folder(folder_path)
    if not document_texts:
        print("Error: No text files found or text could not be loaded.")
        return None

    # Step 2: Embedding the documents
    embeddings = HuggingFaceEmbeddings(model_name="sentence-transformers/all-MiniLM-L6-v2")
    
    # Step 3: Create FAISS index
    print("Creating FAISS index...")
    faiss_index = FAISS.from_texts(document_texts, embeddings)
    
    # Step 4: Set up the retriever with similarity search
    retriever = faiss_index.as_retriever(search_type="similarity", search_kwargs={"k": 10})  # Retrieve top 10 docs
    
    # Step 5: Setup LLM with longer response settings
    print("Setting up language model...")
    llm = HuggingFaceHub(
        repo_id="google/flan-t5-large",  # Use a larger model
        model_kwargs={
            "temperature": 0.7,
            "max_new_tokens": 250,  # Longer responses
            "top_p": 0.9
        },
        huggingfacehub_api_token=huggingfacehub_api_token
    )

    # Step 6: Setup QA chain with custom prompt
    print("Setting up QA chain...")
    custom_prompt = PromptTemplate(
        template="""
        Use the provided context to answer the following question thoroughly and comprehensively.
        If relevant, provide examples or explanations to enhance clarity.

        Context: {context}
        Question: {question}
        Answer:
        """,
        input_variables=["context", "question"]
    )
    
    qa_chain = RetrievalQA.from_chain_type(
        llm=llm,
        retriever=retriever,
        chain_type_kwargs={"prompt": custom_prompt}
    )
    
    return qa_chain

# Run FastAPI server
if __name__ == "__main__":
    import uvicorn

    # Start ngrok tunnel
    public_url = ngrok.connect(8000)
    print(f"Public URL: {public_url.public_url}")  # Use public_url attribute for clarity

    # Run the FastAPI app
    uvicorn.run(app, host="0.0.0.0", port=8000)

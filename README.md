# VISIOLEARN
# AI-Powered AR Learning for Computer Networks

## Overview
This project is an interactive learning tool designed to help students visualize complex computer network concepts using Augmented Reality (AR) and 3D visualizations. It integrates a Retrieval-Augmented Generation (RAG) bot, powered by FastAPI, which provides real-time Q&A support. It also contains MCQ assessments to test students' understanding.

The application is developed using Unity for the AR experience, while the RAG bot is built with FastAPI to ensure smooth interaction between the Unity app and the backend.

## Features
- **Interactive 3D Visualization**: View and interact with computer network concepts in AR.
- **RAG Bot**: Provides real-time answers to student queries, powered by FastAPI and Retrieval-Augmented Generation.
- **MCQ Assessments**: The app also contains multiple-choice questions to assess student understanding.
- **Optimized Performance**: Low-latency responses are ensured by deploying the FastAPI backend locally, integrated seamlessly with Unity through an API.

## Technologies Used
- **Unity**: Used for creating the AR-based 3D visualizations.
- **FastAPI**: Handles the backend for the RAG bot, managing real-time interactions.
- **ngrok**: Deployed FastAPI locally to ensure smooth communication with the Unity app.
- **RAG Technology**: Employed for real-time query response generation.

## Installation

### Prerequisites
- Unity
- Python 3.8+ 
- FastAPI
- ngrok (for local deployment)

### Steps to Run
1. Clone the repository:
    ```bash
    git clone https://github.com/tessara77/visiolearn.git
    cd visiolearn
    ```

2. Install the required Python dependencies:
    ```bash
    pip install -r requirements.txt
    ```

3. Set up Unity and open the Unity project folder.

4. Deploy the FastAPI server locally:
    ```bash
    uvicorn main:app --reload
    ```

5. Use ngrok to expose the FastAPI server:
    ```bash
    ngrok http 8000
    ```

6. Run the Unity project to experience the AR app.

## How It Works
1. **Visualization**: The Unity application renders 3D visualizations of computer network components such as routers, switches, and data packets.
2. **RAG Bot**: The FastAPI-powered RAG bot responds to student questions. It retrieves relevant information from the backend model and presents it to the user.
   
## Contributing
We welcome contributions! If you would like to contribute, feel free to fork the repository, make changes, and create a pull request.

## Acknowledgments
- Special thanks to the open-source community for tools like FastAPI, Unity, and ngrok that made this project possible.



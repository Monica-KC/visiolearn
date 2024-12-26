from transformers import AutoTokenizer, AutoModelForCausalLM

# Load a model
model_name = "EleutherAI/gpt-neo-1.3B"  # Replace with other models if needed
tokenizer = AutoTokenizer.from_pretrained(model_name)
model = AutoModelForCausalLM.from_pretrained(model_name)

# Test the model
prompt = "Generate a multiple-choice question based on the text:\nComputer networks connect devices and transfer data."
inputs = tokenizer(prompt, return_tensors="pt")
outputs = model.generate(inputs["input_ids"], max_length=100, num_return_sequences=1)
print(tokenizer.decode(outputs[0], skip_special_tokens=True))

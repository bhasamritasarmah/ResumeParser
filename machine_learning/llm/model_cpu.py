from text_generation import InferenceAPIClient
from utilities.model_constants import LLM_MODEL, MAX_TOKENS

# Function to call the llm model client
def run_cpu_model(prompt):
    timeout_seconds = 60
    client = InferenceAPIClient(LLM_MODEL, timeout=timeout_seconds)
    output_text = client.generate(prompt, max_new_tokens=MAX_TOKENS, temperature=0.001).generated_text
    return output_text
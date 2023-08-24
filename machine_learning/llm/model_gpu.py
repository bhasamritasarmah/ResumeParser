import transformers
import torch
from utilities.model_constants import LLM_MODEL, MAX_TOKENS


def load_model():
    tokenizer = transformers.AutoTokenizer.from_pretrained(LLM_MODEL)

    pipeline = transformers.pipeline(
        "text-generation",
        model = LLM_MODEL,
        tokenizer = tokenizer,
        torch_dtype = torch.bfloat16,    
        trust_remote_code = True,
        device_map = "auto"
    )

    return [pipeline, tokenizer]


def run_pipeline(pipeline, tokenizer, prompt):
    sequences = pipeline(
        prompt,
        return_full_text = False,
        max_new_tokens = MAX_TOKENS,
        top_k = 1,
        num_return_sequences = 1,
        temperature = 0.0000001,
        eos_token_id = tokenizer.eos_token_id
    )

    output_text = ""
    for seq in sequences:
        output_text += seq['generated_text']

    return output_text



def run_gpu_model(prompt):
    [pipeline, tokenizer] = load_model()
    output_text = run_pipeline(pipeline, tokenizer, prompt)
    return output_text

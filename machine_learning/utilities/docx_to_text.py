import docx

def docx_to_text(resume_file):
    doc = docx.Document(resume_file)
    content = []

    for paragraph in doc.paragraphs:
        content.append(paragraph.text)

    return "\n".join(content)
from flask import Flask

app = Flask(__name__)

@app.route("/home", methods = ["GET"])
def Home():
    return "Welcome to Bhasamrita Sarmah's webpage."

if __name__ == "__main__":
    app.run(host = "localhost", port = 3000)
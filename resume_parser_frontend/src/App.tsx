//Container to embed all React components.

import "bootstrap/dist/css/bootstrap.min.css";
import './App.css';
import FileUpload from "./components/FileUpload";

function App() {
  return (
    <div className="container" style={{ width: "600px" }}>
      <div className="my-3">
        <h3>ResumeParser</h3>
        <h4>Please upload your resume here.</h4>
      </div>
      < FileUpload />
    </div>
  );
}

export default App;

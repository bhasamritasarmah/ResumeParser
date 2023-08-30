import "bootstrap/dist/css/bootstrap.min.css";
import React, { useState } from 'react';
import axios from 'axios';

const baseURL = process.env.REACT_APP_BASE_URL;

function FileUpload() {
  const [file, setFile] = useState<File | null>(null);
  const [uploadProgress, setUploadProgress] = useState(0);
  const [isParsing, setIsParsing] = useState(false);
  const [isSuccess, setIsSuccess] = useState(false); // State for showing success message
  const [isError, setIsError] = useState(false);
  const [errorMessage, setErrorMessage] = useState(''); // State for error message

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files.length > 0) {
      setFile(event.target.files[0]);
    }
  };

  const handleUpload = async () => {
    if (file) {
      const formData = new FormData();
      formData.append('resume', file);

      setIsParsing(true); // Show the parsing alert

      try {
        const response = await axios.post(`${baseURL}/resume/post`, formData, {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
          onUploadProgress: (progressEvent:any) => {
            const progress = Math.round((progressEvent.loaded / progressEvent.total) * 100);
            setUploadProgress(progress);
          },
        });

        console.log('File uploaded successfully:', response.data);
        setIsSuccess(true); // Set success status
      } catch (error) {
        console.error('Error uploading file:', error);
        setIsError(true);
        setErrorMessage('Either the resume upload or parsing was unsuccessful. Please try again.');
      } finally {
        setIsParsing(false); // Hide the parsing alert
      }
    }
  };

  return (
    <div className="container" style={{ width: "600px" }}>
        <div className='my-3'>
            <h1>Resume Parser</h1>
			<h1>--------------------------------</h1>
			<h4>Please upload your resume here - </h4>
        </div>
      <form className='row'>
        <div className='col-8'>
            <input type="file" accept=".pdf, .docx, .txt" onChange={handleFileChange} />
        </div>
        <div className='col-4'>
            <button className="btn btn-success btn-sm" disabled={ !file } type="button" onClick={handleUpload}>
                Upload and Parse
            </button>
        </div>
      </form>
      {isParsing && (
        <div className="alert alert-info mt-3">
          The uploaded resume is being parsed...
        </div>
      )}
      {uploadProgress > 0 && (
        <div className="progress my-3">
          <div
            className="progress-bar progress-bar-info"
            role="progressbar"
            style={{ width: `${uploadProgress}%` }}
            aria-valuenow={uploadProgress}
            aria-valuemin={0}
            aria-valuemax={100}
          >
            {uploadProgress}%
          </div>
        </div>
      )}
	    {isSuccess && (
        <div className="alert alert-success mt-3">
          File Uploaded and Parsed Successfully
        </div>
      )}
      {isError && (
        <div className="alert alert-danger mt-3">
          {errorMessage}
        </div>
      )}
    </div>
  );
}

export default FileUpload;

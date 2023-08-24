//Contains the file upload form, progress bar, display of list files.

import { useState } from "react";
import Upload from "../services/FileUploadService";

/* The function FileUpload implements the part where the file can be uploaded.
 *
 * It uses 3 different useState hooks - one for the file uploaded, the second 
 * for showing the upload progress of the file, and the third for showing the 
 * message once the file is successfully/unsuccessfully uploaded.
 * 
 * The arrow function 'selectFile' takes in a list of files.
 * 
 * The arrow function 'upload' uploads the files one by one and shows the progress
 * in a progress bar. If the file could not be uploaded, it displays an error
 * message. The component Upload (makes the axios post connection) is defined in a
 * separate file 'FileUploadService'. 
 * 
 * The arrow function 'selectFile' is implemented as a file selection button. 
 * The arrow function 'upload' is implemented as a file upload button.
 * The progress bar shows the progress percentage from 0 to 100%.
 * A file upload successful/unsuccessful message is displayed as an alert, once the 
 * file upload process is over.*/

function FileUpload() {
    const [currentFile, setCurrentFile] = useState<File>();
    const [progress, setProgress] = useState<number>(0);
    const [message, setMessage] = useState<string>("");

    const selectFile = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { files } = event.target;
        const selectedFiles = files as FileList;
        setCurrentFile(selectedFiles?.[0]);
        setProgress(0);
    };

    const upload = () => {
        setProgress(0);
        if (!currentFile)   return;

        Upload (currentFile, (event: any) => {
            setProgress(Math.round((100 * event.loaded) / event.total));
        })
        .catch((err) => {
            setProgress(0);

            if(err.response && err.response.data && err.response.data.message){
                setMessage(err.response.data.message);
            }
            else{
                setMessage("Could not upload the File!");
            }

            setCurrentFile(undefined);
        });
    };

    return (
        <>
            <div className="row">
                <div className="col-8">
                    <label className="btn btn-default p-0">
                        <input type="file" onChange={ selectFile } />
                    </label>
                </div>

                <div className="col-4">
                    <button
                        className="btn btn-success btn-sm"
                        disabled={ !currentFile }
                        onClick={ upload }
                    >
                        Upload and Parse
                    </button>
                </div>
            </div>

            { currentFile && (
                <div className="progress my-3">
                    <div
                        className="progress-bar progress-bar-info"
                        role="progressbar"
                        aria-valuenow={ progress }
                        aria-valuemin={ 0 }
                        aria-valuemax={ 100 }
                        style={{ width: progress + "%"}}
                    >
                        { progress }%
                    </div>
                </div>
            )}

            {message && (
                <div className="alert alert-secondary mt-3" role="alert">
                    { message }
                </div>
            )}
        </>
    );
}

export default FileUpload;
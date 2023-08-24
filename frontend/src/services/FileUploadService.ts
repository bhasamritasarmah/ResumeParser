//Provides functions to save File and get File using Axios.

import { uploadURL } from "../endpoints";
import axios from "axios";

/* This function helps to Upload the file to the mentioned URL with the
 * help of the axios post method. It also specifies the type of data to 
 * be uploaded. 
 * 
 * The key value of the formData.append method should match with the 
 * parameter value of the Post method in the C# backend.*/
function Upload(file: File, onUploadProgress: any): Promise<any> {
    let formData = new FormData();

    formData.append("resume", file);
    
    return axios.post(uploadURL, formData, {
        headers: {
            "Content-Type": "multipart/form-data",
        },
        onUploadProgress,
    });
}

export default Upload;
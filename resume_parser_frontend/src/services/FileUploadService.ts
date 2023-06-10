//Provides functions to save File and get File using Axios.

import { uploadURL } from "../endpoints";
import axios from "axios";

function Upload(file: File, onUploadProgress: any): Promise<any> {
    let formData = new FormData();

    formData.append("file", file);
    
    return axios.post(uploadURL, formData, {
        headers: {
            "Content-Type": "multipart/form-data",
        },
        onUploadProgress,
    });
}

export default Upload;
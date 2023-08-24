import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';

/* StrictMode helps find common bugs early during development and hence
 * issues warnings wherever necessary. The whole 'App' component is 
 * rendered using StrictMode. The public/index.html page contains a
 * section called <div id = "root"></div>. So, everything rendered by
 * the react 'App' component will go inside the mentioned div section. */
ReactDOM.render(
  <React.StrictMode>
    <App />       
  </React.StrictMode>,
  document.getElementById("root")
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals

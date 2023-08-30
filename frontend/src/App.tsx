//Container to embed all React components.

import './App.css';
import Navbar from './components/NavBar';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import ListOfResumes from './pages/ResumeLists';
import ResumeDetails from './pages/ResumeDetails';
import RawResume from './pages/RawResume';

/* The function app displays the Navbar or navigation bar, which contains
 * three pages - Home, ListOfResumes, ResumeDetails. Each of these pages
 * are displayed in the Navbar and when clicked on, opens up the particular
 * page. By default, when no particular page is selected, it points to the
 * Home page. The Navbar is visible from all the pages. */
function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/" element={ <Home /> } />
        <Route path="/home" element={ <Home /> } />
        <Route path="/resumelists" element={ <ListOfResumes /> } />
        <Route path="/resumedetails/:id" element={ <ResumeDetails /> } />
        <Route path="/rawresume/:id" element={ <RawResume /> } />
      </Routes>
    </Router>
  );
}

export default App;

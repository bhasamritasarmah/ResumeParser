//Container to embed all React components.

import './App.css';
import Navbar from './components/NavBar';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import About from './pages/About';
import Blogs from './pages/Blogs';
import SignUp from './pages/SignUp';
import Contact from './pages/Contact';

function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/" element={ <Home /> } />
        <Route path="/home" element={ <Home /> } />
        <Route path="/about" element={ <About /> } />
        <Route path="/blogs" element={ <Blogs /> } />
        <Route path="/signup" element={ <SignUp /> } />
        <Route path="/contact" element={ <Contact /> } />
      </Routes>
    </Router>
  );
}

export default App;

import { Nav, NavLink, NavMenu } from "./NavbarElements";

function Navbar() {
    return (
        <>
        <Nav>
            <NavMenu>
                <NavLink to = "/Home"> Home </NavLink>
                <NavLink to = "/About"> About </NavLink>
                <NavLink to = "/Contact"> Contact Us </NavLink>
                <NavLink to = "/Blogs"> Blogs </NavLink>
                <NavLink to = "/SignUp"> Sign Up </NavLink>
            </NavMenu>
        </Nav>
        </>
    );
}

export default Navbar;
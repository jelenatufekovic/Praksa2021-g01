import React from 'react';
import '../App.css';
import '../layouts/Nav.css'
import { Link } from 'react-router-dom';

function Nav() {

    const navStyle = {
        'font-family': 'monospace',
        'color': 'rgb(132, 163, 163)',
        'font-size': '20px',
        'text-decoration': 'none'
    };

    return (
        <nav>
            <div className="nav-links">
                <div className="link-div">
                    <Link style={navStyle} to="/matches">
                        Matches
                    </Link>
                </div>
                <div className="link-div">
                    <Link style={navStyle} to="/standings">
                        Standings
                    </Link>
                </div>
                <div className="link-div">
                    <Link style={navStyle} to="/clubs">
                        Clubs
                    </Link>
                </div>
            </div>
        </nav>
    );
}

export default Nav;

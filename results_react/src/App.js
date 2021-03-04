import React from 'react';
import './App.css';
import Nav from './components/Nav';
import { BrowserRouter as Router, Switch, Route} from 'react-router-dom';
import Matches from './components/Matches';
import Standings from './components/Standings';
import ClubStadium from './components/ClubStadium';

function App() {
    return (
        <Router>
            <div className="App">
                <Nav />
                <Switch>
                    <Route path="/" exact component={Matches} />
                    <Route path="/matches" component={Matches} />
                    <Route path="/standings" component={Standings} />
                    <Route path="/clubs" component={ClubStadium} />
                </Switch>                
            </div>
        </Router>
    );
}

export default App;

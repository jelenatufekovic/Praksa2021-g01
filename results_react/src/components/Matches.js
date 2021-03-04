import React, { Component } from 'react';
import '../App.css';
import '../layouts/Matches.css';

function showImage(team) {
    let image;
switch (team) {
    case "Osijek":
        image = "https://prvahnl.hr/static/images/clubs/mq/osijek.png";
        break;
    case "Varazdin":
        image = "https://prvahnl.hr/static/images/clubs/mq/varazdin.png";
        break;
    case "Dinamo":
        image = "https://prvahnl.hr/static/images/clubs/mq/dinamo.png";
        break;
    case "Slaven Belupo":
        image = "https://prvahnl.hr/static/images/clubs/mq/slaven.png";
        break;
    case "Gorica":
        image = "https://prvahnl.hr/static/images/clubs/mq/gorica.png";
        break;
    case "Istra":
        image = "https://prvahnl.hr/static/images/clubs/mq/istra.png";
        break;
    case "Sibenik":
        image = "https://prvahnl.hr/static/images/clubs/mq/sibenik.png";
        break;
    case "Lokomotiva":
        image = "https://prvahnl.hr/static/images/clubs/mq/lokomotiva.png";
        break;
    case "Rijeka":
        image = "https://prvahnl.hr/static/images/clubs/mq/rijeka.png";
        break;
    case "Hajduk":
        image = "https://prvahnl.hr/static/images/clubs/mq/hajduk.png";
        break;
    default: 
        image = ""
    }

    return image;
}; 

var addMatches = [];

class CountryDetails extends Component {
    constructor(props) {
        super(props);
        this.state = {
            items: [],
            json_body : {
                LeagueSeasonId: "d9673ce8-4d7b-eb11-b566-0050f2ed1c57",
                orderBy: "MatchDate%20desc",
                IsPlayed: "false",
                PageNumber: 1
            },
        };
    }

    fetchMatches = () => {        
        fetch(`https://localhost:44386/api/Match/Get?LeagueSeasonID=${this.state.json_body.LeagueSeasonId}&orderBy=${this.state.json_body.orderBy}&IsPlayed=true&PageNumber=${this.state.json_body.PageNumber}`)
            .then(res => res.json())
            .then(json => {
                console.log(json)
                json.forEach(element => {
                    let newDate = "";
                    newDate = element.MatchDate;
                    newDate = newDate.substring(8,10) + "/" + newDate.substring(5,7) + "/" + newDate.substring(0,4);
                    element.MatchDate = newDate; 
                });
                addMatches = this.state.items.concat(json);
                this.setState({
                    items: addMatches
                });               
                if(addMatches.length > 0) {
                    var numOfPage = this.state.json_body;
                    numOfPage.PageNumber++;
                    console.log(numOfPage.PageNumber);
                    this.setState({json_body: numOfPage})
                }           
            });        
    }
    
    componentDidMount() {
        this.fetchMatches();
      }

    render() {
        return (
            <div className="all_matches_container">
                    {this.state.items.map(item => (
                        <div className="match_container">
                            <div className="match">
                                <div className="match_day">
                                    <h1>Matchday: {item.MatchDay}</h1>
                                </div>
                                <div className="match_date">
                                    <h1>{item.MatchDate}</h1>
                                </div>   
                            </div>
                            <div className="clubs_score">                                
                                <div className="box">
                                    <div className="home_team">
                                        <img src={showImage(item.HomeTeam)} className="club_image"></img>
                                        <p>{item.HomeTeam}</p>
                                    </div>
                                </div>
                                <div className="box">
                                    <div className="score">
                                        <h1>{item.HomeGoals}&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;&nbsp;{item.AwayGoals}</h1>
                                    </div>
                                </div>
                                <div className="box">
                                    <div className="away_team">
                                        <img src={showImage(item.AwayTeam)} className="club_image"></img>
                                        <p>{item.AwayTeam}</p>
                                    </div>
                                </div>    
                            </div>
                        </div>
                    ))}
                    <button onClick={this.fetchMatches} class="fetch">Older matches...</button>
            </div>
        );
    }
}

export default CountryDetails;

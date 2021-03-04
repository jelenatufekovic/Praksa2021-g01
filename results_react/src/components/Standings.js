import { Component } from "react";
import '../layouts/Standings.css';

class Standings extends Component {

  constructor(props){
    super(props);
    this.state ={
      items: [],
      isLoaded: false,
      league: "a620ce44-4d7b-eb11-b566-0050f2ed1c57",
      season: "d7f8a69b-4d7b-eb11-b566-0050f2ed1c57",
      leagueseason:"d9673ce8-4d7b-eb11-b566-0050f2ed1c57",
    };
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(event) {
    //alert("League: " + this.state.league +" Season: "+ this.state.season);
    //event.preventDefault();

    fetch('https://localhost:44386/api/LeagueSeason/Get/Id/?LeagueID=' + this.state.league + '&SeasonID=' + this.state.season)
      .then(res => res.json())
      .then(json => {
        this.setState({
          leagueseason: json.Id,
        })
      })

      event.preventDefault();
  }

  handleChangeLeague = event => {
    this.setState({ 
      league: event.target.value, 
    });
  };

  handleChangeSeason = event => {
    this.setState({ 
      season: event.target.value, 
    });
  };

  componentDidMount(){
    fetch('https://localhost:44386/api/Standings/Get/'+ this.state.leagueseason)
        .then(res => res.json())
        .then(json => {
          this.setState({
            isLoaded: true,
            items: json,
          })
        })
  }

  renderTableData() {
    return this.state.items.map((items, index) => {
       const {ClubID, ClubName, Played, Won, Draw, Lost, GoalsScored, GoalsConceded, Points } = items //destructuring

       return (
          <tr key={ClubID}>
             <td>{ClubName}</td>
             <td>{Played}</td>
             <td>{Won}</td>
             <td>{Draw}</td>
             <td>{Lost}</td>
             <td>{GoalsScored}</td>
             <td>{GoalsConceded}</td>
             <td>{Points}</td>
          </tr>
       )
    })
  }

  
  renderTableHeader() {
    let header = Object.keys(this.state.items[0])
    return header.map((key, index) => {
      return <th key={index}>{key}</th>
    }).slice(1)
  }


  render(){
    var { isLoaded, items } = this.state;
    if(!isLoaded){
      return <div>Loading...</div>
    }
    else{
      return (
        <form onSubmit={this.handleSubmit}>
          <h2 id='header2'> 
            <select id='select' value={this.state.league} onChange={this.handleChangeLeague}>
              <option value="a620ce44-4d7b-eb11-b566-0050f2ed1c57">HNL</option>
              <option value="leag">LaLiga</option>
            </select>
            <select id='select' value={this.state.season} onChange={this.handleChangeSeason}>
              <option value="d7f8a69b-4d7b-eb11-b566-0050f2ed1c57">Season 2020/2021</option>
              <option value="seas">Season 2019/2020</option>
            </select>
            <input id='select' type="submit" value="Show" />
          </h2>
          <table id='standings'>
            <tbody>
              <tr>{this.renderTableHeader()}</tr>
              {this.renderTableData()}
            </tbody>
          </table>
        </form>
      )
    }
  }
}

export default Standings;

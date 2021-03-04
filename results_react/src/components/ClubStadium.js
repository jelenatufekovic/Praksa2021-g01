import React, { Component } from "react";
import {Card, Container, Row, Col, Button} from "react-bootstrap";
import axios from 'axios';
import { Modal, ModalHeader, ModalBody, ModalFooter, Input, FormGroup, Label } from "reactstrap";
import '../layouts/ClubStadium.css'

class ClubStadium extends Component {

    state = {
        clubs: [],
        newClubData: {
            Name: '',
            ShortName: '',
            ClubAddress: '',
            YearOfFoundation: '',
            Description: '',
        },
        newStadiumData: {
            Name: '',
            StadiumAddress: '',
            Capacity: '',
            YearOfConstruction: '',
            Description: ''
        },
        newClubStadiumModal: false
    }

    componentWillMount() {
        axios.get('https://localhost:44386/api/club/FindClubs').then((response) => {
            this.setState({
                clubs: response.data
            })
          }).catch(errors => {
              console.error(errors);
          })
    }

    toggleNewClubStadiumModal() {
        this.setState({
            newClubStadiumModal: !this.state.newClubStadiumModal
        });
    }

    addClubStadium() {
        let data = JSON.stringify({
            Name: this.state.newStadiumData.Name,
            StadiumAddress: this.state.newStadiumData.StadiumAddress,
            Capacity: this.state.newStadiumData.Capacity,
            YearOfConstruction: this.state.newStadiumData.YearOfConstruction,
            Description: this.state.newStadiumData.Description
        });
        axios.post('https://localhost:44386/api/stadium/CreateStadium', data,{headers:{"Content-Type" : "application/json"}}).then((response) => {
            console.log(response.data);
        });
    }

    render() {

        let clubs = this.state.clubs.map((club) => {
            return (
                <section className="line">
                    <Row key={club.Id} style={{width: '100%', paddingTop: '20px', paddingBottom: '20px'}}>
                        <Col>
                            <Card style={{ width: '100%', height: '460px' }}>
                                <Card.Body>
                                    <img src={process.env.PUBLIC_URL+'/images/'+ club.Name + '.png'} alt=""></img>
                                    <Card.Title style={{paddingTop: '15px'}}>{club.Name} ({club.ShortName})</Card.Title>
                                    <Card.Subtitle className="mb-2 text-muted">Adresa: {club.ClubAddress}</Card.Subtitle>
                                    <Card.Subtitle className="mb-2 text-muted">Godina osnivanja: {club.YearOfFoundation}</Card.Subtitle>
                                    <Card.Subtitle className="mb-2 text-muted" style={{paddingTop: '27px'}}>Opis</Card.Subtitle>
                                    <Card.Text className="text">
                                    {club.Description}
                                    </Card.Text>
                                </Card.Body>
                            </Card>
                        </Col>
                        <Col>
                            <Card style={{ width: '100%', height: '460px' }}>
                                <Card.Body>
                                    <img src={process.env.PUBLIC_URL+'/images/'+ club.Stadium.Name + '.jpg'} alt=""></img>
                                    <Card.Title style={{paddingTop: '15px'}}>{club.Stadium.Name}</Card.Title>
                                    <Card.Subtitle className="mb-2 text-muted">Adresa: {club.Stadium.StadiumAddress}</Card.Subtitle>
                                    <Card.Subtitle className="mb-2 text-muted">Kapacitet: {club.Stadium.Capacity}</Card.Subtitle>
                                    <Card.Subtitle className="mb-2 text-muted">Godina izgradnje: {club.Stadium.YearOfConstruction}</Card.Subtitle>
                                    <Card.Subtitle className="mb-2 text-muted" style={{paddingTop: '3px'}}>Opis</Card.Subtitle>
                                    <Card.Text className="text">
                                    {club.Stadium.Description}
                                    </Card.Text>
                                </Card.Body>
                            </Card>
                        </Col>
                        <Button style={{ borderRadius: '0px' }} variant="danger" size="sm">Delete</Button>
                    </Row>
                </section>
            )
        });

        return (
            <div className="App container" style={{ paddingTop: "20px" }}>
                <Container>
                    {clubs}
                    <div className="addClubStadium" style={{paddingTop: '20px', paddingBottom: '40px'}}>
                        <Button style={{ height: '50px', width: '67%', borderRadius: '0px' }} variant="success" block onClick={this.toggleNewClubStadiumModal.bind(this)}>Add new Club and Stadium</Button>
                        {/* <Modal isOpen={this.state.newClubStadiumModal} toggle={this.toggleNewClubStadiumModal.bind(this)}>
                            <ModalHeader toggle={this.toggleNewClubStadiumModal.bind(this)}>Add Club and Stadium</ModalHeader>
                            <ModalBody>
                                <FormGroup>
                                    <Label for="ClubName">Club Name</Label>
                                    <Input id="ClubName" placeholder="Club Name" value={this.state.newClubData.Name} onChange={(e) => {
                                        let { newClubData } = this.state;
                                        newClubData.Name = e.target.value;
                                        this.setState({ newClubData });
                                    }} />
                                </FormGroup>
                                <FormGroup>
                                    <Label for="ClubAddress">Club Address</Label>
                                    <Input id="ClubAddress" placeholder="Club Address" value={this.state.newClubData.ClubAddress} onChange={(e) => {
                                        let { newClubData } = this.state;
                                        newClubData.ClubAddress = e.target.value;
                                        this.setState({ newClubData });
                                    }}/>
                                </FormGroup>
                                <FormGroup>
                                    <Label for="ShortName">Short Name</Label>
                                    <Input id="ShortName" placeholder="Short Name" value={this.state.newClubData.ShortName} onChange={(e) => {
                                        let { newClubData } = this.state;
                                        newClubData.ShortName = e.target.value;
                                        this.setState({ newClubData });
                                    }}/>
                                </FormGroup>
                                <FormGroup>
                                    <Label for="YearOfFoundation">Year Of Foundation</Label>
                                    <Input id="YearOfFoundation" placeholder="Year Of Foundation" value={this.state.newClubData.YearOfFoundation} onChange={(e) => {
                                        let { newClubData } = this.state;
                                        newClubData.YearOfFoundation = e.target.value;
                                        this.setState({ newClubData });
                                    }}/>
                                </FormGroup>
                                <FormGroup>
                                    <Label for="ClubDescription">Club Description</Label>
                                    <Input id="ClubDescription" placeholder="Club Description" value={this.state.newClubData.Description} onChange={(e) => {
                                        let { newClubData } = this.state;
                                        newClubData.Description = e.target.value;
                                        this.setState({ newClubData });
                                    }} />
                                </FormGroup>
                                <FormGroup>
                                    <Label for="StadiumName">Stadium Name</Label>
                                    <Input id="StadiumName" placeholder="Stadium Name" value={this.state.newStadiumData.Name} onChange={(e) => {
                                        let { newStadiumData } = this.state;
                                        newStadiumData.Name = e.target.value;
                                        this.setState({ newStadiumData });
                                    }}/>
                                </FormGroup>
                                <FormGroup>
                                    <Label for="StadiumAddress">Stadium Address</Label>
                                    <Input id="StadiumAddress" placeholder="Stadium Address" value={this.state.newStadiumData.StadiumAddress} onChange={(e) => {
                                        let { newStadiumData } = this.state;
                                        newStadiumData.StadiumAddress = e.target.value;
                                        this.setState({ newStadiumData });
                                    }}/>
                                </FormGroup>
                                <FormGroup>
                                    <Label for="Capacity">Capacity</Label>
                                    <Input id="Capacity" placeholder="Capacity" value={this.state.newStadiumData.Capacity} onChange={(e) => {
                                        let { newStadiumData } = this.state;
                                        newStadiumData.Capacity = e.target.value;
                                        this.setState({ newStadiumData });
                                    }}/>
                                </FormGroup>
                                <FormGroup>
                                    <Label for="YearOfConstruction">Year Of Construction</Label>
                                    <Input id="YearOfConstruction" placeholder="Year Of Construction" value={this.state.newStadiumData.YearOfConstruction} onChange={(e) => {
                                        let { newStadiumData } = this.state;
                                        newStadiumData.YearOfConstruction = e.target.value;
                                        this.setState({ newStadiumData });
                                    }}/>
                                </FormGroup>
                                <FormGroup>
                                    <Label for="StadiumDescription">Stadium Description</Label>
                                    <Input id="StadiumDescription" placeholder="Stadium Description" value={this.state.newStadiumData.Description} onChange={(e) => {
                                        let { newStadiumData } = this.state;
                                        newStadiumData.StadiumDescription = e.target.value;
                                        this.setState({ newStadiumData });
                                    }}/>
                                </FormGroup>
                            </ModalBody>
                            <ModalFooter>
                            <Button style color="primary" onClick={this.addClubStadium.bind(this)}>Do Something</Button>{' '}
                            <Button color="secondary" onClick={this.toggleNewClubStadiumModal.bind(this)}>Cancel</Button>
                            </ModalFooter>
                        </Modal> */}
                    </div>
                </Container>
            </div>
        )
    }
}

export default ClubStadium;

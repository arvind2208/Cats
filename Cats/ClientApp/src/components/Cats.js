import React, { Component } from 'react';
import './Cats.css';

export class Cats extends Component {
    displayName = Cats.name

  constructor(props) {
    super(props);
      this.state = {catsByOwnersGenders: [], loading: true };

    fetch('api/cats')
      .then(response => response.json())
      .then(data => {
          this.setState({ catsByOwnersGenders: data.catsByOwnersGenders, loading: false });
          console.log(data);
      });
  }

    static renderCatsByOwnersGendersTable(catsByOwnersGenders) {
    return (
        <div>
		  {catsByOwnersGenders.map(g =>
		  <div >
			<h2>{g.ownerGender}</h2>
			<ul>
			{g.cats.map(c =>
				<li>{c}</li>
			)}
			</ul>
		  </div>
		  )}
		</div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : Cats.renderCatsByOwnersGendersTable(this.state.catsByOwnersGenders);

    return (
      <div>
        <h1>Cats by Owner's Gender</h1>
        <p>This component lists cats by their owner's gender.</p>
        {contents}
      </div>
    );
  }
}

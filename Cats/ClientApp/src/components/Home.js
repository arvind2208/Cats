import React, { Component } from 'react';

export class Home extends Component {
  displayName = Home.name

  render() {
    return (
      <div>
        <h1>Pets World!</h1>
        <p>Welcome to pets world</p>
        <ul>
          <li>Click <strong><em>Cats By Owner's Gender</em></strong> to view cats by their owner's gender</li>
        </ul>
      </div>
    );
  }
}

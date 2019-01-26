import React from 'react';
import ReactDOM from 'react-dom';
import Cats from './Cats';

it('renders without crashing', () => {
    const div = document.createElement('div');
    ReactDOM.render(<Cats />, div);
    ReactDOM.unmountComponentAtNode(div);
});

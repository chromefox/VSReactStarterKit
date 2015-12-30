// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

var React = require('react');
var ReactDOM = require('react-dom');

export class InfoBox {
    reactComponent:any;

    constructor() {
        this.reactComponent = React.createClass({
            render() {
                return (
                    <div className="alert alert-info">
                       {this.props.text}
                        </div>
                );
            }
        });
    }

    domRender(domId: string, domText: string) {
        ReactDOM.render(
            <reactComponent url="/data.json" submitUrl="/React/CreateComment" pollInterval={2000} />,
            document.getElementById('content')
        );
    }
}
// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

var React = require('react');
var ReactDOM = require('react-dom');

var AlertComponent = React.createClass({
    render() {
        return (
            <div className="alert alert-info">
                       {this.props.text}
                </div>
        );
    }
});

export class InfoBox {
    domRender(domId: string, domText: string) {
        ReactDOM.render(
            <AlertComponent text={domText} />,
            document.getElementById(domId)
        );
    }
}
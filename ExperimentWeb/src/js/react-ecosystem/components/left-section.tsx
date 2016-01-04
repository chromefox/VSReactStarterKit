declare var global: Global;
var React = require('react');

export class LeftSection {
    getComponent() {
        return React.createClass({
            getInitialState() {
                return { title: "N/A" };
            },
            handleClick(titleStr) {
                this.setState({ title: titleStr });
            },
            addMapMarker() {
                global.mapClass.addNewMarker(global.map);
            },
            render() {
                return (
                    <div id="left-section" className="col-lg-6">
                        <h3>Create a new marker</h3>
                        <button id="newMarkerDemo" className="button" onClick={this.addMapMarker} >Click</button>
                        <h3>Details section</h3>
                        <div id="markerDetail">{this.state.title}</div>
                     </div>
                );
            }
        });
    }
}
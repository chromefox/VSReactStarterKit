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

var TableComponent = React.createClass({
    render() {
        var rows = this.props.data.map(row => {
            return (<tr>
                <td>
                    {row.name}
                    </td>
                    <td>
                    {row.email}
                        </td>
                    <td>
                    {row.year}
                        </td>
                </tr>);
        });

        return (
            <table className="dataTable table table-bordered">
        <thead>
            <tr>
                <th>Name</th>
                <th>Email</th>
                <th>Year</th>
                </tr>
            </thead>
        <tbody>
            {rows}
            </tbody>
                </table>
        );
    }
});

var PageComponent = React.createClass({
    render() {
        return (
            <div>
                <AlertComponent text={this.props.alertText} />
                <TableComponent data={this.props.tableData} />
            </div>
        );
    }
});


export class LabsPage {
    domRender(domId: string, domText: string, tableData: Object) {
        ReactDOM.render(
            <PageComponent alertText={domText} tableData={tableData} />,
            document.getElementById(domId)
        );
    }
}
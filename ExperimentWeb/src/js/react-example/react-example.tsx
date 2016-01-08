// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

var React = require('react');
var ReactDOM = require('react-dom');

// serve as a templating language
var AlertComponent = React.createClass({
    render() {
        return (
            <div className="alert alert-info">
                       {this.props.text}
                </div>
        );
    }
});

// serve as a templating language with basic events
var TableComponent = React.createClass({
    render() {
        var rows = this.props.data.map(row => {
            return (
                <tr key={row.name}>
                    <td onClick={this.clickHandler }>
                        {row.name}
                        </td>
                    <td>
                        {row.email}
                        </td>
                    <td>
                        {row.year}
                        </td>
                    </tr>
            );
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
    },
    clickHandler() {
        alert("I am clicked");
    }
});

var TextInputComponent = React.createClass({
    render() {
        var additionalAttributes = {}
        if (this.props.inputAttributes) {
            additionalAttributes = this.props.inputAttributes;
        }

        return (
            <div>
                <input {...additionalAttributes} type="text" />
                <span className="field-validation-valid text-danger"  data-valmsg-replace="true" data-valmsg-for="Test"></span>
            </div>
        );
    }
});


var SubmitButton = React.createClass({
    render() {
        return (
            <input type="submit" />
        );
    }
});

var FormComponent = React.createClass({
    render() {
        return (
            <form>
                <TextInputComponent {...this.props} />
                <SubmitButton />
             </form>
        );
    }
});

var PageComponent = React.createClass({
    render() {
        return (
            <div>
                <AlertComponent text={this.props.alertText} />
                <TableComponent data={this.props.tableData} />
                <FormComponent inputAttributes={this.props.inputAttributes}  />
                </div>
        );
    }
});

export class LabsPage {
    domRender(domId: string, domText: string, tableData: Object, inputAttributes: Object) {
        ReactDOM.render(
            <PageComponent alertText={domText} tableData={tableData} inputAttributes={inputAttributes}  />,
            document.getElementById(domId)
        );
    }
}
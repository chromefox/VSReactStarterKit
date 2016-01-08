import * as React from 'react';

class MainSectionTSX extends React.Component<{}, {}> {
    test() {
        alert("Alerted!");
    }

    render() {
        return (
            <div>
                <section className="main">
                    Main Section from TSX
                    </section>
                <button className="button" onClick={this.test.bind(this) }>Click to alert</button>
            </div>
        );
    }
}

export default MainSectionTSX;
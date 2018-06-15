import * as React from 'react'

interface IRegisterProps {
}

interface IRegisterState {
}

export class Register extends React.Component<IRegisterProps, IRegisterState> {
    constructor(props: any) {
        super(props);
        this.state = {
        }
    }

    render() {
        return (
                <div id="signupbox" className="mainbox">
                    <div className="panel panel-info">
                        <div className="panel-heading">
                            <div className="panel-title">Sign Up</div>
                        </div>
                        <div className="panel-body" >
                            <form id="signupform" className="form-horizontal" role="form">

                                <div id="signupalert" className="alert alert-danger">
                                    <p>Error:</p>
                                    <span></span>
                                </div>

                                <div className="form-group">
                                <div className="row">
                                    <label className="col-md-3 control-label">User name</label>
                                    <div className="col-md-9">
                                        <input type="text" className="form-control" name="icode" placeholder="User Name" />
                                    </div>
                                    </div>
                                </div>

                                <div className="form-group">
                                <div className="row">
                                    <label className="col-md-3 control-label">Email</label>
                                    <div className="col-md-9">
                                        <input type="text" className="form-control" name="email" placeholder="Email Address" />
                                    </div>
                                    </div>
                                </div>

                                <div className="form-group">
                                <div className="row">
                                    <label className="col-md-3 control-label">First Name</label>
                                    <div className="col-md-9">
                                        <input type="text" className="form-control" name="firstname" placeholder="First Name" />
                                    </div>
                                    </div>
                                </div>
                                <div className="form-group">
                                <div className="row">
                                    <label className="col-md-3 control-label">Last Name</label>
                                    <div className="col-md-9">
                                        <input type="text" className="form-control" name="lastname" placeholder="Last Name" />
                                    </div>
                                    </div>
                                </div>
                                <div className="form-group">
                                <div className="row">
                                    <label className="col-md-3 control-label">Password</label>
                                    <div className="col-md-9">
                                        <input type="password" className="form-control" name="passwd" placeholder="Password" />
                                    </div>
                                    </div>
                                </div>
                                <div className="form-group">
                                <div className="row">
                                    <label className="col-md-3 control-label">Confirm Password</label>
                                    <div className="col-md-9">
                                        <input type="password" className="form-control" name="passwd" placeholder="Confirm Password" />
                                    </div>
                                    </div>
                                </div>

                                <div className="form-group">
                                    <div className="col-md-offset-3 col-md-9">
                                        <button id="btn-signup" type="button" className="btn btn-info"><i className="icon-hand-right"></i> Sign Up</button>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>

                </div>
        )
    }
}
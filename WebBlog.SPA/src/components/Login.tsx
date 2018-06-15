import * as React from 'react'

interface ILoginProps {
}

interface ILoginState {
}

export class Login extends React.Component<ILoginProps, ILoginState> {
    constructor(props: any) {
        super(props);
        this.state = {
        }
    }

    render() {
        return (
            <div id="loginbox" className="mainbox">
                <div className="panel panel-info">
                    <div className="panel-heading">
                        <div className="panel-title">Sign In</div>
                    </div>

                    <div className="panel-body" >

                        <div id="login-alert" className="alert alert-danger col-sm-12">
                            <p>Error:</p>
                            <span></span>
                        </div>

                        <form id="loginform" className="form-horizontal" role="form">
                            <div className="input-group">
                                <span className="input-group-addon"><i className="glyphicon glyphicon-user"></i></span>
                                <input id="login-username" type="text" className="form-control" name="username" value="" placeholder="username or email" />
                            </div>
                            <br />
                            <div className="input-group">
                                <span className="input-group-addon"><i className="glyphicon glyphicon-lock"></i></span>
                                <input id="login-password" type="password" className="form-control" name="password" placeholder="password" />
                            </div>
                            <br />
                            <div className="input-group">
                                <div className="checkbox">
                                    <label>
                                        <input id="login-remember" type="checkbox" name="remember" value="1" /> Remember me
                                        </label>
                                </div>
                            </div>

                            <div className="form-group">

                                <div className="col-sm-12 controls">
                                    <a id="btn-login" href="#" className="btn btn-success">Login</a>
                                </div>
                            </div>

                            <div className="form-group">
                                <div className="col-md-12 control">
                                    <div >
                                        Don't have an account!
                                        <a href="#">
                                            Sign Up Here
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </form>

                    </div>
                </div>
            </div>
        )
    }
}
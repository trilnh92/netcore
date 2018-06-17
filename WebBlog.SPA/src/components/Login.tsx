import * as React from 'react'
import { Link, Redirect } from "react-router-dom"
import { apiLoginAccount } from './../apiService'
import { parseJwt } from './../helper'

interface ILoginProps {
    setUserProfile: any;
}

interface ILoginState {
    loginModel: any;
    errorMessage: string;
    redirectToMyProfile: boolean;
}

export class Login extends React.Component<ILoginProps, ILoginState> {
    constructor(props: any) {
        super(props);
        this.state = {
            loginModel: {},
            errorMessage: '',
            redirectToMyProfile: false
        }
    }

    setUserProfile = (profile: any) => {
        this.props.setUserProfile(profile);
    }

    handleTextChange = (event: any, name: string) => {
        this.state.loginModel[name] = event.target.value;
        this.setState({ loginModel: this.state.loginModel });
    }

    login = (event: any) => {
        // This is not good solution. Need to research more...
        event.preventDefault();
        this.setState({ errorMessage: '' });

        var loginModel = {
            "grant_type": "password",
            "username": this.state.loginModel.userName,
            "password": this.state.loginModel.password,
            "scope": "api1 openid",
            "client_id": "WebBlogAuthentication",
            "client_secret": "T2iHoangWebBlog@123!"
        }

        apiLoginAccount(loginModel, (response: any) => {

            if (response.target.status == 200) {
                let data = JSON.parse(response.target.responseText);
                const token = parseJwt(data.access_token)
                const userInfo = {
                    "firstName": token.given_name,
                    "lastName": token.family_name,
                    "email": token.email
                }
                this.setState({ redirectToMyProfile: true });
                this.setUserProfile(userInfo);
            }
        },
            (errors: any) => {
                 this.setState({ redirectToMyProfile: false });
                this.setState({ errorMessage: 'Error when login account!' });
            })

    }

    render() {
        if (this.state.redirectToMyProfile) {
            return <Redirect to='/myprofile' />;
        }
        return (
            <div>
                <h1 className="my-4">Login
                </h1>
                <div className="panel-body" >

                    {
                        (() => {
                            if (this.state.errorMessage && this.state.errorMessage != '') {
                                return (
                                    <div id="login-alert" className="alert alert-danger col-sm-12">
                                        <p>{this.state.errorMessage}</p>
                                        <span></span>
                                    </div>
                                )
                            }
                        })()
                    }

                    <form id="loginform" className="form-horizontal" role="form">
                        <div className="input-group">
                            <span className="input-group-addon"><i className="glyphicon glyphicon-user"></i></span>
                            {/* <input id="login-username" type="text" className="form-control" name="username" value="" placeholder="username or email" /> */}
                            <input type="text"
                                value={this.state.loginModel.userName ? this.state.loginModel.userName : ""}
                                onChange={evt => this.handleTextChange(evt, 'userName')}
                                className="form-control" placeholder="username or email" />
                        </div>
                        <br />
                        <div className="input-group">
                            <span className="input-group-addon"><i className="glyphicon glyphicon-lock"></i></span>
                            {/* <input id="login-password" type="password" className="form-control" name="password" placeholder="password" /> */}
                            <input type="password"
                                value={this.state.loginModel.password ? this.state.loginModel.password : ""}
                                onChange={evt => this.handleTextChange(evt, 'password')}
                                className="form-control" placeholder="password" />
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
                                {/* <a id="btn-login" href="#" className="btn btn-success">Login</a> */}
                                <button className="btn btn-success" type="button"
                                    onClick={(e) => this.login(e)}
                                >Login</button>
                            </div>
                        </div>

                        <div className="form-group">
                            <div className="col-md-12 control">
                                <div >
                                    Don't have an account!
                                        <Link className="nav-link" to="/register">Sign up here</Link>
                                </div>
                            </div>
                        </div>
                    </form>

                </div>
            </div>
        )
    }
}
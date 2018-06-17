import * as React from "react";
import { Route, Link } from "react-router-dom"
import { Search } from "./Search";
import { Category } from "./Category";
import { Article } from "./Article";
import { MainArticles } from "./MainArticles";
import { Register } from "./Register";
import { Login } from "./Login";
import { MyProfile } from "./MyProfile";
import { MyBlogs } from "./MyBlogs";
import { CreateBlog } from "./CreateBlog";

import { UserManager } from 'oidc-client'

interface IAppPros {

}

interface IAppState {
    userProfile: any;
    userManager:any;
}

export class Layout extends React.Component<IAppPros, IAppState> {

    constructor(props: any) {
        super(props);

        //Initial config application
        var config = {
            authority: "http://localhost:52960",
            client_id: "WebBlogAuthentication",
            redirect_uri: "http://localhost:5000/callback.html",
            response_type: "id_token token",
            scope:"openid profile api1",
            post_logout_redirect_uri : "http://localhost:5000/",
        };

        const retrievedUserProfile = localStorage.getItem('userProfile');

        this.state = {
            userProfile: retrievedUserProfile?JSON.parse(retrievedUserProfile):undefined,
            userManager: new UserManager(config)
        }
    }

    logOut = (e: any) => {
        e.preventDefault();
        this.setState({ userProfile: undefined });
        localStorage.removeItem('userProfile');
    }

    setUserProfile = (profile: any) => {
        this.setState({ userProfile: profile });
        //Set to local storage
        localStorage.setItem('userProfile', JSON.stringify(profile));
    }

    render() {
        return (
            <div>
                <nav className="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">
                    <div className="container">
                        {/* <a className="navbar-brand" href="#">WebBlog</a> */}
                        <Link className="navbar-brand" to="/">WebBlog</Link>
                        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse" id="navbarResponsive">
                            {
                                (() => {
                                    if (this.state.userProfile != undefined) {
                                        return (
                                            <ul className="navbar-nav ml-auto">
                                                <li className="nav-item active">
                                                    <Link className="nav-link" to="/myblogs">My blogs</Link>
                                                </li>
                                                <li className="nav-item active">
                                                    <Link className="nav-link" to="/createblog">Write blog</Link>
                                                </li>
                                                <li className="nav-item">
                                                    <Link className="nav-link" to="/myprofile">My profile</Link>
                                                </li>
                                                <li className="nav-item">
                                                    <a className="nav-link" href="#" onClick={(e) => this.logOut(e)}>Logout </a>
                                                </li>
                                            </ul>
                                        )
                                    } else {
                                        return (
                                            <ul className="navbar-nav ml-auto">

                                                <li className="nav-item">
                                                    <Link className="nav-link" to="/login">Login</Link>
                                                </li>
                                                <li className="nav-item">
                                                    <Link className="nav-link" to="/register">Register</Link>
                                                </li>
                                            </ul>
                                        )
                                    }
                                })()
                            }
                        </div>
                    </div>
                </nav>

                <div className="container">
                    <div className="row">
                        <div className="col-lg-8">
                            <Route path="/" exact component={MainArticles} />
                            <Route path="/article/:articleId" component={Article} />
                            <Route path="/login"component={() => <Login setUserProfile={(profile: any) => this.setUserProfile(profile)} />} />
                            <Route path="/register" component={() => <Register setUserProfile={(profile: any) => this.setUserProfile(profile)} />} />
                            <Route path="/myprofile" component={() => <MyProfile myProfile={this.state.userProfile} />} />
                            <Route path="/myblogs" component={() => <MyBlogs myProfile={this.state.userProfile} />} />
                            <Route path="/createblog" component={() => <CreateBlog userProfile={this.state.userProfile} />} />
                        </div>

                        <div className="col-md-4">
                            <Search />
                            <Category />
                        </div>
                    </div>

                </div>

                <footer className="py-5 bg-dark">
                    <div className="container">
                        <p className="m-0 text-center text-white">Copyright &copy; WebBlog 2018</p>
                    </div>
                </footer>

            </div>
        )
    }
}

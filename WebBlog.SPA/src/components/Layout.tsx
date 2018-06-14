import * as React from "react";
import { BrowserRouter } from "react-router-dom"
import { Route } from "react-router-dom"
import { Search } from "./Search";
import { Category } from "./Category";
import { Article } from "./Article";
import { MainArticles } from "./MainArticles";

export class Layout extends React.Component {

    render() {
        return (
            <div>
                <nav className="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">
                    <div className="container">
                        <a className="navbar-brand" href="#">WebBlog</a>
                        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse" id="navbarResponsive">
                            <ul className="navbar-nav ml-auto">
                                <li className="nav-item active">
                                    <a className="nav-link" href="#">Home
                                         <span className="sr-only">(current)</span>
                                    </a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="#">About</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="#">Services</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="#">Contact</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>

                <div className="container">
                    <div className="row">
                        <div className="col-lg-8">
                            {/* <MainArticles /> */}
                            {/* <Article/> */}
                            <Route path="/" exact component={MainArticles} />
                            <Route path="/article/:articleId" component={Article} />   
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

import * as React from 'react'
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import {changeBrokenImage } from './../helper'

interface IArticleSummaryProps {
    article:any;
}

interface IArticleSummaryState {
    article: any;
}

export class ArticleSummary extends React.Component<IArticleSummaryProps, IArticleSummaryState> {
    constructor(props: any) {
        super(props);
        this.state = {
            article: this.props.article
        }
    }

    render() {
        return (
            <div className="card mb-4">
                <img className="card-img-top" src={this.state.article.image} alt="Card image cap" onError={(e) => changeBrokenImage(e.target)}/>
                <div className="card-body">
                    <h2 className="card-title">{this.state.article.title}</h2>
                    <p className="card-text">{this.state.article.briefContent}</p>
                    <Link to={"/article/" + this.state.article.articleId}>Read more</Link>
                </div>
                <div className="card-footer text-muted">
                    Posted on {this.state.article.createdDate} by
                <span> <a href="#"> {this.state.article.createdBy}</a></span>
                </div>
            </div>
        )
    }
}
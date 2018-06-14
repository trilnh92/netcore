import * as React from 'react'
import { BrowserRouter as Router, Route, Link } from "react-router-dom";

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
                <img className="card-img-top" src="http://placehold.it/750x300" alt="Card image cap" />
                <div className="card-body">
                    <h2 className="card-title">{this.state.article.title}</h2>
                    <p className="card-text">{this.state.article.briefContent}</p>
                    <Link to={"/article/" + this.state.article.articleId}>Read more</Link>
                </div>
                <div className="card-footer text-muted">
                    Posted on {this.state.article.createdDate} by
                <a href="#">{this.state.article.createdBy}</a>
                </div>
            </div>
        )
    }
}
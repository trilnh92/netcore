import * as React from 'react'
import { ArticleSummary } from './ArticleSummary';
import { apiGetArticlesByUser, apiGetArticlesByUserPaging } from '../apiService';
import { Redirect } from 'react-router-dom';
import { BaseUrl } from './../base.url'

interface IMyBlogsProps {
    myProfile: any;
}

interface IMyBlogsState {
    articles: any;
    pageIndex: number;
}

export class MyBlogs extends React.Component<IMyBlogsProps, IMyBlogsState> {
    constructor(props: any) {
        super(props);
        this.state = {
            articles: [],
            pageIndex: 1
        }
    }

    loadArticles = (page: number) => {
        let email = this.props.myProfile ? this.props.myProfile.email : '';
        let userModel = { "Email": email };

        apiGetArticlesByUserPaging(page, userModel, (response: any) => {
            if (response.target.status == 200) {
                let data = JSON.parse(response.target.responseText);
                this.setState({ articles: data })
            }
        },
            (errors: any) => {
                this.setState({ articles: [] });
            })
    }

    loadOlder = (e: any) => {
        e.preventDefault();
        let newState = { ...this.state };
        let pageIndex = newState.pageIndex + 1;

        this.loadArticles(pageIndex);
        this.setState(newState)
    }

    loadNewer = (e: any) => {
        e.preventDefault();
        let newState = { ...this.state };
        let pageIndex = newState.pageIndex > 1 ? newState.pageIndex - 1 : 1;

        this.loadArticles(pageIndex);
        this.setState(newState)
    }

    componentDidMount() {
        this.loadArticles(1);
    }

    render() {
        if (this.props.myProfile == undefined || this.props.myProfile.email == undefined) {
            return <Redirect to={BaseUrl.HOME_URL} />;
        }

        return (
            <div>
                <h1 className="my-4">My blogs
                </h1>

                {this.state.articles && this.state.articles.map((article: any, i: number) => {
                    return (
                        <div key={i}>
                            <ArticleSummary article={article} key={article.articleId} />
                        </div>
                    )
                })}

                <ul className="pagination justify-content-center mb-4">
                    <li className="page-item">
                        <a className="page-link" onClick={(e: any) => { this.loadOlder(e) }}>&larr; Older</a>
                    </li>
                    <li className="page-item">
                        <a className="page-link" onClick={(e: any) => { this.loadNewer(e) }}>Newer &rarr;</a>
                    </li>
                </ul>
            </div>
        )
    }
}
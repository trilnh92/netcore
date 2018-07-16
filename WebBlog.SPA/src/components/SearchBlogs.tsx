import * as React from 'react'
import { ArticleSummary } from './ArticleSummary';
import { RouteComponentProps } from "react-router";
import { apiSearchArticlesPaging } from '../apiService';
import { Redirect } from 'react-router-dom';
import { BaseUrl } from './../base.url'

// interface ISearchBlogsProps extends RouteComponentProps{
//     category:string;
// }

interface ISearchBlogsState {
    articles: any;
    searchKey: string;
    pageIndex: number;
}

export class SearchBlogs extends React.Component<RouteComponentProps<any>, ISearchBlogsState> {
    constructor(props: any) {
        super(props);
        this.state = {
            articles: [],
            searchKey: '',
            pageIndex: 1
        }
    }

    loadArticles = (search: string, page: number) => {
        apiSearchArticlesPaging(search, page, (response: any) => {
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

        this.loadArticles(this.state.searchKey, pageIndex);
        this.setState(newState)
    }

    loadNewer = (e: any) => {
        e.preventDefault();
        let newState = { ...this.state };
        let pageIndex = newState.pageIndex > 1 ? newState.pageIndex - 1 : 1;

        this.loadArticles(this.state.searchKey, pageIndex);
        this.setState(newState)
    }

    componentDidMount() {
        const searchKey = this.props.match.params.search ? this.props.match.params.search : '';
        this.setState({ searchKey: searchKey });
        this.loadArticles(searchKey, 1);
    }

    render() {
        return (
            <div>
                <h1 className="my-4">{this.state.searchKey ? this.state.searchKey : ''}
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
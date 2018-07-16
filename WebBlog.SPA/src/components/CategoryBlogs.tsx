import * as React from 'react'
import { ArticleSummary } from './ArticleSummary';
import { RouteComponentProps } from "react-router";
import { apiGetArticlesByCategory, apiGetArticlesByCategoryPaging } from '../apiService';
import { Redirect } from 'react-router-dom';
import { BaseUrl } from './../base.url'

// interface ICategoryBlogsProps extends RouteComponentProps{
//     category:string;
// }

interface ICategoryBlogsState {
    articles: any;
    categoryName: string;
    pageIndex: number;
}

export class CategoryBlogs extends React.Component<RouteComponentProps<any>, ICategoryBlogsState> {
    constructor(props: any) {
        super(props);
        this.state = {
            articles: [],
            categoryName: '',
            pageIndex: 1
        }
    }

    loadArticles = (category: string, page: number) => {
        apiGetArticlesByCategoryPaging(category, page, (response: any) => {
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

        this.loadArticles(this.state.categoryName, pageIndex);
        this.setState(newState)
    }

    loadNewer = (e: any) => {
        e.preventDefault();
        let newState = { ...this.state };
        let pageIndex = newState.pageIndex > 1 ? newState.pageIndex - 1 : 1;

        this.loadArticles(this.state.categoryName, pageIndex);
        this.setState(newState)
    }

    componentDidMount() {
        const category = this.props.match.params.category ? this.props.match.params.category : '';
        this.setState({ categoryName: category });
        this.loadArticles(category, 1);
    }

    render() {
        return (
            <div>
                <h1 className="my-4">{this.state.categoryName ? this.state.categoryName : ''}
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
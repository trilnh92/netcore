import * as React from 'react'
import { ArticleSummary } from './ArticleSummary';
import { RouteComponentProps } from "react-router";
import { apiGetArticlesByCategory } from '../apiService';
import { Redirect } from 'react-router-dom';
import {BaseUrl} from './../base.url'

// interface ICategoryBlogsProps extends RouteComponentProps{
//     category:string;
// }

interface ICategoryBlogsState {
    articles: any;
    categoryName:string;
}

export class CategoryBlogs extends React.Component<RouteComponentProps<any>, ICategoryBlogsState> {
    constructor(props: any) {
        super(props);
        this.state = {
            articles: [],
            categoryName:''
        }
    }

    loadArticles = (category:string) => {
        apiGetArticlesByCategory(category, (response: any) => {
            if (response.target.status == 200) {
                let data = JSON.parse(response.target.responseText);
                this.setState({ articles: data })
            }
        },
            (errors: any) => {
                this.setState({ articles: [] });
            })
    }
    
    componentDidMount() {
        const category = this.props.match.params.category ? this.props.match.params.category : '';	
        this.setState({categoryName:category});	
        this.loadArticles(category);        
    }

    render() {
        return (
            <div>
                <h1 className="my-4">{this.state.categoryName?this.state.categoryName:''}
                </h1>

                {this.state.articles && this.state.articles.map((article: any, i: number) => {
                    return (
                        <div key={i}>
                            <ArticleSummary article={article}/>
                        </div>
                    )
                })}

                <ul className="pagination justify-content-center mb-4">
                    <li className="page-item">
                        <a className="page-link" href="#">&larr; Older</a>
                    </li>
                    <li className="page-item disabled">
                        <a className="page-link" href="#">Newer &rarr;</a>
                    </li>
                </ul>
            </div>
        )
    }
}
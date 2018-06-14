import * as React from 'react'
import { ArticleSummary } from './ArticleSummary';
import { apiGetArticles } from '../apiService';
import { ArticleModel } from '../models/article.model';

interface IMainArticlesProps {
}

interface IMainArticlesState {
    articles: any;
}

export class MainArticles extends React.Component<IMainArticlesProps, IMainArticlesState> {
    constructor(props: any) {
        super(props);
        this.state = {
            articles: []
        }
    }

    loadArticles = () => {
        apiGetArticles((response: any) => {
            if (response.target.status == 200) {
                let data = JSON.parse(response.target.responseText);
                this.setState({ articles: data })
            }
        },
            (errors: any) => {
                this.setState({ articles: [] });
            })
    }

    componentWillMount() {
        this.loadArticles();
    }

    render() {
        return (
            <div>
                <h1 className="my-4">Information technologies
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
import * as React from 'react'
import { ArticleSummary } from './ArticleSummary';
import { apiGetArticlesByUser } from '../apiService';
import { Redirect } from 'react-router-dom'

interface IMyBlogsProps {
    myProfile:any;
}

interface IMyBlogsState {
    articles: any;
}

export class MyBlogs extends React.Component<IMyBlogsProps, IMyBlogsState> {
    constructor(props: any) {
        super(props);
        this.state = {
            articles: []
        }
    }

    loadArticles = () => {
        let email = this.props.myProfile?this.props.myProfile.email:'';
        let userModel = {"Email":email};

        apiGetArticlesByUser(userModel, (response: any) => {
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
        this.loadArticles();
    }

    render() {
        if(this.props.myProfile == undefined || this.props.myProfile.email == undefined){
            return <Redirect to='/' />;
        }

        return (
            <div>
                <h1 className="my-4">My blogs
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
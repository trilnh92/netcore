import * as React from 'react';
import { apiGetCategories } from '../apiService'

interface ICategoryProps {
}

interface ICategoryState {
  categories: any;
}

export class Category extends React.Component<ICategoryProps, ICategoryState> {
  constructor(props: any) {
    super(props);
    this.state = {
      categories: []
    }
  }

  loadCategories = () => {
    apiGetCategories((response: any) => {
      if (response.target.status == 200) {
        let data = JSON.parse(response.target.responseText);
        this.setState({ categories: data })
      }
    },
      (errors: any) => {
        this.setState({ categories: [] });
      })
  }

  componentWillMount() {
    this.loadCategories();
  }

  render() {
    return (
      <div className="card my-4">
        <h5 className="card-header">Categories</h5>
        <div className="card-body">
          <div className="row">
            <div className="col-lg-6">
              <ul className="list-unstyled mb-0">
                <li>
                  <a href="#">Web Design</a>
                </li>
                <li>
                  <a href="#">HTML</a>
                </li>
                <li>
                  <a href="#">Freebies</a>
                </li>
              </ul>
            </div>
            <div className="col-lg-6">
              <ul className="list-unstyled mb-0">
                <li>
                  <a href="#">JavaScript</a>
                </li>
                <li>
                  <a href="#">CSS</a>
                </li>
                <li>
                  <a href="#">Tutorials</a>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    )
  }
}
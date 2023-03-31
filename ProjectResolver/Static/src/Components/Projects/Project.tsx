import React from "react";
import ProjectProps from "../../Models/Project"

const Project: React.FC<ProjectProps> = props => {

    return <div className="project-container">
        <div className="row">
            <div className="col">
                {props.name}
            </div>
            <div className="col">
                {props.version}
            </div>
            <div className="col">
                {props.currentVersion}
            </div>
        </div>
    </div>
};

export default Project;
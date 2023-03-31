import ProjectsProps from "../../Models/Projects";
import React from "react";

const List: React.FC<ProjectsProps> = (props) => {

  return (
    <div className="project-list">
      <ul className="list-group">
        {props.projects?.map((project, key) => (
          <li className="list-group-item list-group-item-active" key={key}>
            {project.name}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default List;

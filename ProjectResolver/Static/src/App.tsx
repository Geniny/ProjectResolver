import * as React from "react";
import ProjectsList from "./Components/Projects/List";
import "bootstrap/dist/css/bootstrap.min.css";
import Project from "./Models/Project";
import Projects from "./Models/Projects";
import ApiClient from "./Api/Client";

const App: React.FC = (props) => {
  const [projects, setProjects] = React.useState<Project[] | undefined>()

  React.useEffect(() => {
    const onProjectsLoaded = (data: Project[]) => {
			setProjects(data)
		};

		new ApiClient().get("https://localhost:7210/Projects", (data: Project[]) => onProjectsLoaded(data), console.log);
  }, []);

  return (
    <div className="App">
      <ProjectsList projects={projects}/>
    </div>
  );
};

export default App;

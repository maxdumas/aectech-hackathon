<h1 align="center">
  <br>
  AllTheWayDown
  <br>
</h1>

![Rhinos ATWD](/Static/stacked-rhinos-sculpture.png)

<h4 align="center">Dependency management and version control to link every phase of design.</h4>

<p align="center">
  <a href="#why">Why AllTheWayDown?</a> •
  <a href="#key-features">Key Features</a> •
  <a href="#how-to-use">How To Use</a> •
  <a href="#credits">Credits</a> •
  <a href="#license">License</a>
</p>

![screenshot]()

## AllTheWayDown

AllTheWayDown (ATWD) allows for managing dependencies within design workflows, which decreases length of time between design iterations, introduces Git-based version control, and eliminates discrepancies between different stages of design.  Initially, ATWD works for Grasshopper script graphs and Rhino models. Future iterations will expand to other design tools and data inputs.  

ATWD was developed at the AEC Tech New York Hackathon 2023 hosted by Thornton Tomasetti CORE studio. The hackathon team included:
- [Chau Nguyen](https://github.com/minhchau1510) - [Foster + Partners](https://www.fosterandpartners.com)
- [Keyan Rahimzadeh](https://github.com/keyan-r) - Front
- [Patryk Wozniczka](https://github.com/patrykwoz) - 
- [Nathan Barnes](https://github.com/nathan-barnes) - [Zahner](https://www.azahner.com/)
- [Sarang Pramode](https://github.com/Sarang-Pramode) - [Ulama](https://ulama.tech)
- [Tyce Herrman](https://github.com/TyceHerrman) - [Ulama](https://ulama.tech)
- [Max Dumas](https://github.com/maxdumas) - [Ulama](https://ulama.tech)

## Key Features

* Dependency Updates 🕸️
  - When you make changes to one or more Grasshopper scripts, any script dependencies will be updated upon.
* Version control 🚧
  - While you type, LivePreview will automatically scroll to the current location you're editing.
* Dependency Graph Visualization 📈
  - See your entire dependency graph and understand how changes are propagated through the graph.
  ![graph](/GraphView%20Image.png)


## How To Use

To clone and run this application, you'll need [Git](https://git-scm.com) installed on your computer. You'll also need [Data Version Control](https://dvc.org/doc/install).

From your command line:

```bash
# Clone this repository
$ git clone https://github.com/maxdumas/aectech-hackathon.git

# Navigate to directory
cd aectech-hackathon

# Create Mermaid diagram of DVC DAG with:
$ dvc dag --mermaid > output.mmd

# Navigate to GraphView directory

cd GraphView

# Run mermaid-to-D3-structure.py
pdm run mermaid-to-D3-structure.py

# Start local http server within the GraphView directory (for >Python3)
python -m http.server 5000

```


## Credits

This software uses the following open source packages:

- [Data Version Control](https://dvc.org/)

It also uses

- [Rhinocommon](https://www.rhino3d.com)
- [Grasshopper](https://www.grasshopper3d.com)



## License

MIT

---


import re

# The content of your .mmd file
# mmd_content = """
# flowchart TD
#     node1["extract_text"]
#     node2["generate_dataset"]
#     node3["generate_text_dataset"]
#     node4["index_towns"]
#     node5["upload_zoning_docs"]
#     node1-->node2
#     node2-->node3
#     node3-->node4
#     node5-->node1
#     node3-->node1
# """

#read the content of the output.mmd file and store it in a String
with open('output.mmd', 'r') as file:
    mmd_content = file.read()



# Parse nodes and links from the .mmd content
node_pattern = re.compile(r'node\d+\["(.*?)"\]')
link_pattern = re.compile(r'(node\d+)-->(node\d+)')

nodes = set()
links = set()

for node_match in node_pattern.finditer(mmd_content):
    nodes.add(node_match.group(1))

for link_match in link_pattern.finditer(mmd_content):
    source, target = link_match.groups()
    links.add((source.replace('node', ''), target.replace('node', '')))

print(links)

# Map node numbers to their IDs
node_mapping = {f"{i+1}": node_id for i, node_id in enumerate(nodes)}


# Convert sets to list for consistent ordering
nodes_list = [{"id": node_id} for node_id in nodes]
links_list = [{"source": node_mapping[source], "target": node_mapping[target]} for source, target in links]

# Generate the JS data structure and save to .txt files
nodes_js = f"{nodes_list}"
links_js = f"{links_list}"

#convert all single quotes to double quotes
nodes_js = nodes_js.replace("'", '"')
links_js = links_js.replace("'", '"')

with open('nodes.txt', 'w') as nodes_file:
    nodes_file.write(nodes_js)

with open('links.txt', 'w') as links_file:
    links_file.write(links_js)

print("Nodes and links have been saved into nodes.txt and links.txt respectively.")

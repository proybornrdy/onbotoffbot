import os
import sys
import jsonz

for i in range(1, len(sys.argv)):
    file_path =  sys.argv[i]
    print(file_path)

    with open(file_path) as f:
        contents = json.load(f)

    file_split = file_path.split('.')
    out_file_path = '.'.join(file_split[:-1] + ["pretty"] + [file_split[-1]])
    with open(out_file_path, 'w') as f:
        json.dump(contents, f, sort_keys=True, indent=4)

print("done")

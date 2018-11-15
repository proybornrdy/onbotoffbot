import os
import json
import sys

path = "C:\\Users\\mshul\\AppData\\LocalLow\\DefaultCompany\\On Bot Off Bot"
for filename in os.listdir(path):
    if filename[-4:] != "json":
        continue
    print(filename)

    with open(os.path.join(path, filename)) as f:
        data = json.load(f)
    print([v for v in data["startTime"]])

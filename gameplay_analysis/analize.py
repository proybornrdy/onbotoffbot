import os
import json
import sys
import glob

# path = "C:\\Users\\mshul\\AppData\\LocalLow\\DefaultCompany\\On Bot Off Bot"
path = "./data/*"
all_data = []
print("Reading:")
for filepath in glob.glob(path):
    if filepath[-4:] != "json":
        continue
    print(filepath)

    with open(filepath) as f:
        data = json.load(f)

    if len(data['onPositions']) != len(data['offPositions']):
        print("On Positions and Off Positoins do not match")
        continue

    all_data.append(data)

time_per_room = []
count_per_room = []
for data in all_data:
    while len(time_per_room) < len(data["RoomClearTimes"]):
        time_per_room.append(0)
        count_per_room.append(0)

    for i in range(len(data["RoomClearTimes"])):
        time_per_room[i] += data["RoomClearTimes"]
        count_perroom[i] += 1

print()
print("Room Clear Times:")
for i in range(len(time_per_room)):
    print("Room {}: {}s per room for {} rooms".format(i, time_per_room[i] / count_per_room[i], count_per_room[i]))

print()
print("Stage Clear Times:")
clear_time = 0
clear_count = 0
for data in all_data:
    if len(data["onPositions"]) != 0:
        clear_time += len(data["onPositions"])
        clear_count += 1

print("Average Clear Time: {:.02f}s over {} stages".format(clear_time / clear_count / 60, clear_count))

import matplotlib.pyplot as plt
import matplotlib
matplotlib.use('Agg')
with open('reg.txt') as f:
	lines = f.readlines()
	x = [float(line.split(',')[0]) for line in lines]
	y = [float(line.split(',')[2]) for line in lines]

#print(x)

for q in range(len(x)):
	if x[q] >180:
		x[q]=x[q]-360
#print(x)


plt.plot(y,x)

plt.autoscale()

plt.xlabel('time')
# naming the y axis
plt.ylabel('x-coor')

# giving a title to my graph
plt.title('Non-Spastic')


plt.savefig("matplotlib.png")

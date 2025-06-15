import os
import matplotlib.pyplot as plt
import matplotlib as mpl

# Ustawienia stylu
plt.style.use("seaborn-v0_8-whitegrid")  # delikatna siatka
mpl.rcParams.update({
    "font.family": "serif",
    "font.size": 12,
    "axes.titlesize": 14,
    "axes.labelsize": 12,
    "axes.linewidth": 1.2,
    "grid.linewidth": 0.6,
    "xtick.labelsize": 11,
    "ytick.labelsize": 11,
    "lines.linewidth": 2,
    "lines.markersize": 6
})

folder = "logs_edge"
sizes = [3, 10, 20, 30, 50, 100, 150, 200, 500, 1000]
avg_times = []

# Histogramy z ulepszonym stylem
for size in sizes:
    path = os.path.join(folder, f"{size}.txt")
    if not os.path.exists(path):
        continue

    with open(path, "r") as f:
        times = [int(line.strip()) for line in f if line.strip().isdigit()]

    if len(times) == 0:
        continue

    avg = sum(times) / len(times)
    avg_times.append(avg)

    # plt.figure(figsize=(6, 4))
    # plt.hist(times, bins=10, edgecolor='black', color='skyblue')
    # plt.title(f"Histogram czasu dodania bloku.\nLiczba pomiarów: {size}")
    # plt.xlabel("Czas [ms]")
    # plt.ylabel("Liczba przypadków")
    # plt.grid(True, linestyle='--', alpha=0.7)
    # plt.tight_layout()
    # plt.savefig(f"{folder}/hist_{size}.png", dpi=300)
    # plt.close()

# Wykres średnich czasów – wersja podstawowa
plt.figure(figsize=(6, 4))
plt.plot(sizes[:len(avg_times)], avg_times, marker='o', color='navy')
plt.title("Średni podpisania paczki danych\n w zalezności do liczby pomiarów")
plt.xlabel("Liczba pomiarów")
plt.ylabel("Średni czas [ms]")
plt.grid(True, linestyle='--', alpha=0.7)
plt.tight_layout()
plt.savefig(f"{folder}/avg_plot.png", dpi=300)

# Wykres z podpisanymi punktami
plt.figure(figsize=(6, 4))
plt.plot(sizes[:len(avg_times)], avg_times, marker='o', linestyle='-', color='darkred')
for x, y in zip(sizes[:len(avg_times)], avg_times):
    plt.text(x, y + max(avg_times) * 0.015, f"{y:.1f} ms", ha='center', fontsize=10)
plt.title("Średni czas dodania bloku\n w zależności od liczby pomiarów")
plt.xlabel("Liczba pomiarów w bloku")
plt.ylabel("Średni czas [ms]")
plt.grid(True, linestyle='--', alpha=0.7)
plt.tight_layout()
plt.savefig(f"{folder}/avg_plot_annotated.png", dpi=300)
plt.show()

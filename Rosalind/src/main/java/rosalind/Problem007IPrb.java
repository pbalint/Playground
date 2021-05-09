package rosalind;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class Problem007IPrb {
	private enum GeneDominance {
		HOMOZYGOTE_DOMINANT,
		HETEROZYGOTE,
		HOMOZYGOTE_RECESSIVE;
	}
	
	private static class Organism {
		private Set<Organism> potentialMates = new HashSet<>();
		private GeneDominance geneDominance;
		
		public Organism(GeneDominance geneDominance) {
			this.geneDominance = geneDominance;
		}

		public GeneDominance getGeneDominance() {
			return geneDominance;
		}
		
		public Set<Organism> getPotentialMates() {
			return potentialMates;
		}
	}

	public static void main(String[] args) throws IOException {
		String input = "27 15 20";
		String[] parts = input.split(" ");
		int homoZygDom = Integer.parseInt(parts[0]);
		int heteroZyg = Integer.parseInt(parts[1]);
		int homoZygRec = Integer.parseInt(parts[2]);
		
		List<Organism> population = new ArrayList<>();
		for (int i = 0; i < homoZygDom; i++) {
			population.add(new Organism(GeneDominance.HOMOZYGOTE_DOMINANT));
		}
		for (int i = 0; i < heteroZyg; i++) {
			population.add(new Organism(GeneDominance.HETEROZYGOTE));
		}
		for (int i = 0; i < homoZygRec; i++) {
			population.add(new Organism(GeneDominance.HOMOZYGOTE_RECESSIVE));
		}
		for (int i = 0; i < population.size(); i++) {
			Organism organism = population.get(i);
			for (int j = 0; j < population.size(); j++) {
				if (organism != population.get(j)) {
					organism.getPotentialMates().add(population.get(j));
				}
			}
		}

		double dominantAlleleInheritanceChance = 0;
		int matingCombinations = 0;
		for (int i = 0; i < population.size(); i++) {
			Organism organism = population.get(i);
			Set<Organism> potentialMates = new HashSet<>(organism.getPotentialMates());
			for (Organism potentialMate : potentialMates) {
				dominantAlleleInheritanceChance += getDominantAlleleChance(organism, potentialMate);
				matingCombinations++;
				organism.getPotentialMates().remove(potentialMate);
				potentialMate.getPotentialMates().remove(organism);
			}
		}
		System.out.println(dominantAlleleInheritanceChance / matingCombinations);
	}
	
	private static double getDominantAlleleChance(Organism o1, Organism o2) {
		if (o1.getGeneDominance() == GeneDominance.HOMOZYGOTE_DOMINANT || o2.getGeneDominance() == GeneDominance.HOMOZYGOTE_DOMINANT) {
			return 1;
		}
		else if (o1.getGeneDominance() == GeneDominance.HETEROZYGOTE) {
			if (o2.getGeneDominance() == GeneDominance.HETEROZYGOTE) {
				return 0.75;
			}
			else {
				return 0.5;
			}
		}
		else if (o2.getGeneDominance() == GeneDominance.HETEROZYGOTE) {
			return 0.5;
		}
		return 0;
	}
}

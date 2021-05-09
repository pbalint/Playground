package rosalind;

public class Problem011Fibd {

	public static void main(String[] args) {
		int iterations = 86;
		int rabbitMaxAge = 20;
		long[] population = new long[rabbitMaxAge];
		population[0] = 1; 
		for (int i = 0; i < iterations - 1; i++) {
			population[0] = passTimeAndGetOffspring(population);
			System.out.println(sum(population));
		}
	}
	
	private static long sum(long[] population) {
		long sum = 0;
		for (int i = 0; i < population.length; i++) {
			sum += population[i];
		}
		return sum;
	}

	private static long passTimeAndGetOffspring(long[] populationHistory) {
		long offspring = 0;
		for (int generation = populationHistory.length - 1; generation >= 1; generation--) {
			offspring += populationHistory[generation];
			populationHistory[generation] = populationHistory[generation - 1];
		}
		return offspring;
	}

}

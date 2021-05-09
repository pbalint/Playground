package rosalind;

public class Problem004Fib {

	public static void main(String[] args) {
		int iterations = 35;
		int breedingRateInPairs = 4;
		long matingPairs = 1;
		long breedingPairs = 0;
		for (int i = 0; i < iterations; i++) {
			long prevMatingPairs = matingPairs;
			matingPairs += breedingPairs * breedingRateInPairs;
			breedingPairs = prevMatingPairs;
			System.out.println(breedingPairs);
		}
	}

}

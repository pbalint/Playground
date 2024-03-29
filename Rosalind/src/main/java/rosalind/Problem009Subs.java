package rosalind;

import java.util.ArrayList;
import java.util.List;

public class Problem009Subs {
	public static void main(String[] args)  {
		String string = "CTGATGCCTTAGGCCTTAGGCCTTAGTCAAAGCCTTAGTGCCTTAGGCCTTAGATATCGCTTTCCCACTCAAAGCCTTAGGCCTTAGATCATCTCTTGCCTTAGGGGCCTTAGCGCCTTAGACGCCTTAGCTCCAGCCTTAGAGCCTTAGGCCTTAGCAGCCTTAGTGGCCTTAGGGGCCTTAGCGGCCTTAGGCCTTAGTGCCTTAGGACTATGCCTTAGTGCCTTAGACATCCTGCCTTAGAAACCCGTGCCTTAGGCGCCTTAGGGGCCTTAGCTGCCTTAGGATTCCGCCTTAGACTGGCCTTAGGACTACAACATGCAGCCTTAGACTGCCTTAGTTCTCCTAGGCCTTAGGCCTTAGGCCTTAGAGTGCCTTAGCCCCAGCCTTAGAGTAGCCTTAGGCCTTAGGCCTTAGGCCTTAGGCCTTAGGCCTTAGTGGAGCCTTAGTGCCTTAGTCGCCTTAGAAGCCTTAGATCGTTTGGCCTTAGGTCGCGGCCTTAGTCGCCTTAGCGGGCCTTAGGGGCCTTAGCGCCTTAGCAGCCTTAGTTTGCCTTAGCGGGCGCCTTAGGCGCGCCTTAGTTGCCTTAGCAGCCTTAGCTCCGTCGGCCTTAGCCGCCTTAGGCCTTAGCCCCGGCCTTAGAGCGCCTTAGATGCCTTAGATCCCTAGCCTTAGACACGGCCTTAGTGAAAGTTCGCCTTAGGGCCTTAGGGATGCTGTCTCGTTTGCCTTAGGGCCTTAGGCCTTAGGCCTTAGACCAGCCTTAGGCCTTAGGCCTTAGTGCCTTAGGGGGCCTTAGCATGCGTGCCTTAGGAAGCCTTAGGCCTTAGAGCCTTAGGATCCTCTCTT";
		String substring = "GCCTTAGGC";
		
		List<Integer> occurences = new ArrayList<>();
		for (int i = 0; i < string.length() - substring.length(); i++) {
			boolean match = true;
			for (int j = 0; j < substring.length(); j++) {
				if (string.charAt(i + j) != substring.charAt(j)) {
					match = false;
					break;
				}
			}
			if (match) {
				occurences.add(i + 1);
			}
		}
		occurences.forEach(e -> System.out.print(e + " "));
		System.out.println();
	}
	
}

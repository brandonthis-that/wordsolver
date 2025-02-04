from itertools import permutations
from collections import defaultdict

# todo
# I need to update the console UI
# Add colors and lines to the console UI


def load_words(filename="words.txt"):
    with open(filename, "r") as f:
        return set(word.strip().lower() for word in f)


def generate_words(letters, word_set):
    valid_words = defaultdict(set)  # Use set to remove duplicates

    for length in range(2, len(letters) + 1):
        for perm in permutations(letters, length):
            word = "".join(perm)
            if word in word_set:
                valid_words[len(word)].add(word)

    return {
        length: sorted(words) for length, words in valid_words.items()
    }  # Sort words alphabetically


def display_words(grouped_words):
    for length in sorted(grouped_words.keys(), reverse=True):  # Sort by length
        print(f"\n{length}-letter words:")
        print(", ".join(grouped_words[length]))


def main():
    word_set = load_words()
    letters = input("Enter the letters: ").lower()

    grouped_words = generate_words(letters, word_set)

    if grouped_words:
        display_words(grouped_words)
    else:
        print("No words found!")


if __name__ == "__main__":
    main()
